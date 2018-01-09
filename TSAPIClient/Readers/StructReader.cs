using System;
using System.IO;
using System.Runtime.InteropServices;
using NLog;

namespace TSAPIClient.Readers
{
    public interface IStructReader
    {
        bool TryReadStruct(Type type, out object result);
        int ReadInt32();
        uint ReadUInt32();
        bool ReadBoolean();
        short ReadInt16();
        byte ReadByte();
        Stream BaseStream { get; }
    }

    public class StructReader : BinaryReader, IStructReader
    {
        private static readonly Logger logger = new NullLogger(new LogFactory());

        internal StructReader(byte[] buffer) : base(new MemoryStream(buffer))
        {
        }

        internal StructReader(Stream stream) : base(stream)
        {
        }

        public bool TryReadStruct(Type type, out object result)
        {
            result = null;
            IntPtr ptr = IntPtr.Zero;

            try
            {
                logger.Info("StructReader.TryReadStruct: Type={0}", type.Name);

                bool bIsStruct = type.IsValueType && !type.IsEnum;

                // determine if this type is a struct

                logger.Info("StructReader.TryReadStruct: IsStruct={0}", bIsStruct);

                // if this type is not a struct, exit
                if (!bIsStruct)
                {
                    return false;
                }

                // create an instance of the struct
                result = Activator.CreateInstance(type);

                logger.Info("StructReader.TryReadStruct: Instance={0}", result);

                // get the size in bytes of the struct
                int size = Marshal.SizeOf(result);

                logger.Info("StructReader.TryReadStruct: Size={0}", size);

                if (size > 0)
                {
                    logger.Info("StructReader.TryReadStruct: check length of stream...");
                    logger.Info("StructReader.TryReadStruct: BaseStream.Length={0};BaseStream.Position={1}", BaseStream.Length, BaseStream.Position);
                    // make sure there are enough byte left in the stream to read the struct
                    if ((BaseStream.Length - BaseStream.Position) >= size)
                    {
                        logger.Info("StructReader.TryReadStruct: read bytes from stream...");

                        // read the appropriate number of bytes from the underlying stream
                        byte[] bytes = ReadBytes(size);

                        logger.Info("StructReader.TryReadStruct: bytes={0}", bytes);

                        // make sure the correct number of bytes were read
                        if (bytes.Length == size)
                        {
                            logger.Info("StructReader.TryReadStruct: allocate memory...");
                            // allocate memory for the struct bytes
                            ptr = Marshal.AllocHGlobal(size);

                            logger.Info("StructReader.TryReadStruct: ptr={0}", ptr);

                            if (ptr != IntPtr.Zero)
                            {
                                logger.Info("StructReader.TryReadStruct: copy the bytes to the allocated memory...");
                                // copy the bytes to the allocated memory
                                Marshal.Copy(bytes, 0, ptr, size);                                   

                                logger.Info("StructReader.TryReadStruct: convert the pointer to the struct type...");
                                // convert the pointer to the struct type
                                result = Marshal.PtrToStructure(ptr, type);

                                logger.Info("StructReader.TryReadStruct: result={0}", result);

                                // success!
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                logger.Error(string.Format("Error in StructReader.TryReadStruct: {0}", err));
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    // release the allocated memory
                    Marshal.FreeHGlobal(ptr);
                }
            }

            return false;
        }
    }
}
