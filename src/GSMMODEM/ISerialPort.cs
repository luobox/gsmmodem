using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace GSMMODEM
{
    public interface ISerialPort
    {
        //
        // 摘要:
        //     获取或设置串行波特率。
        //
        // 返回结果:
        //     波特率。
        //
        // 异常:
        //   System.ArgumentOutOfRangeException:
        //     指定的波特率小于或等于零，或者大于设备所允许的最大波特率。
        //
        //   System.IO.IOException:
        //     此端口处于无效状态。- 或 - 尝试设置基础端口状态失败。例如，从此 System.IO.Ports.SerialPort 对象传递的参数无效。
        int BaudRate { get; set; }
        int DataBits { get; set; }
        //
        // 摘要:
        //     获取或设置一个值，该值在串行通信过程中启用数据终端就绪 (DTR) 信号。
        //
        // 返回结果:
        //     如果为 true，则启用数据终端就绪 (DTR)；否则为 false。默认值为 false。
        //
        // 异常:
        //   System.IO.IOException:
        //     此端口处于无效状态。 - 或 - 尝试设置基础端口状态失败。例如，从此 System.IO.Ports.SerialPort 对象传递的参数无效。
        bool DtrEnable { get; set; }
        //
        // 摘要:
        //     获取或设置串行端口数据传输的握手协议。
        //
        // 返回结果:
        //     System.IO.Ports.Handshake 值之一。默认值为 None。
        //
        // 异常:
        //   System.IO.IOException:
        //     此端口处于无效状态。 - 或 - 尝试设置基础端口状态失败。例如，从此 System.IO.Ports.SerialPort 对象传递的参数无效。
        //
        //   System.ArgumentOutOfRangeException:
        //     传递的值不是 System.IO.Ports.Handshake 枚举中的有效值。
        //
        //   System.InvalidOperationException:
        //     流已关闭。这可能会因为尚未调用 System.IO.Ports.SerialPort.Open() 方法或已调用了 System.IO.Ports.SerialPort.Close()
        //     方法而发生。
        Handshake Handshake { get; set; }
        //
        // 摘要:
        //     获取一个值，该值指示 System.IO.Ports.SerialPort 对象的打开或关闭状态。
        //
        // 返回结果:
        //     如果串行端口已打开，则为 true；否则为 false。默认值为 false。
        //
        // 异常:
        //   System.ArgumentNullException:
        //     传递的 System.IO.Ports.SerialPort.IsOpen 值为 null。
        //
        //   System.ArgumentException:
        //     传递的 System.IO.Ports.SerialPort.IsOpen 值是空字符串 ("")。
        bool IsOpen { get; }
        //
        // 摘要:
        //     获取或设置用于解释 System.IO.Ports.SerialPort.ReadLine() 和 System.IO.Ports.SerialPort.WriteLine(System.String)
        //     方法调用结束的值。
        //
        // 返回结果:
        //     表示行尾的值。默认值为换行符 (System.Environment.NewLine)。
        //
        // 异常:
        //   System.ArgumentException:
        //     属性值为空。
        //
        //   System.ArgumentNullException:
        //     属性值为 null。
        string NewLine { get; set; }
        Parity Parity { get; set; }
        string PortName { get; set; }
        //
        // 摘要:
        //     获取或设置读取操作未完成时发生超时之前的毫秒数。
        //
        // 返回结果:
        //     读取操作未完成时发生超时之前的毫秒数。
        //
        // 异常:
        //   System.IO.IOException:
        //     此端口处于无效状态。 - 或 - 尝试设置基础端口状态失败。例如，从此 System.IO.Ports.SerialPort 对象传递的参数无效。
        //
        //   System.ArgumentOutOfRangeException:
        //     读取超时值小于零，且不等于 System.IO.Ports.SerialPort.InfiniteTimeout。
        int ReadTimeout { get; set; }
        bool RtsEnable { get; set; }
        StopBits StopBits { get; set; }
        // 摘要:
        //     表示将处理 System.IO.Ports.SerialPort 对象的数据接收事件的方法。
        event SerialDataReceivedEventHandler DataReceived;

        // 摘要:
        //     关闭端口连接，将 System.IO.Ports.SerialPort.IsOpen 属性设置为 false，并释放内部 System.IO.Stream
        //     对象。
        //
        // 异常:
        //   System.InvalidOperationException:
        //     指定的端口未打开。
        void Close();
        //
        // 摘要:
        //     丢弃来自串行驱动程序的接收缓冲区的数据。
        //
        // 异常:
        //   System.IO.IOException:
        //     此端口处于无效状态。 - 或 -尝试设置基础端口状态失败。例如，从此 System.IO.Ports.SerialPort 对象传递的参数无效。
        //
        //   System.InvalidOperationException:
        //     流已关闭。这可能会因为尚未调用 System.IO.Ports.SerialPort.Open() 方法或已调用了 System.IO.Ports.SerialPort.Close()
        //     方法而发生。
        void DiscardInBuffer();
        //
        // 摘要:
        //     丢弃来自串行驱动程序的传输缓冲区的数据。
        //
        // 异常:
        //   System.IO.IOException:
        //     此端口处于无效状态。 - 或 - 尝试设置基础端口状态失败。例如，从此 System.IO.Ports.SerialPort 对象传递的参数无效。
        //
        //   System.InvalidOperationException:
        //     流已关闭。这可能会因为尚未调用 System.IO.Ports.SerialPort.Open() 方法或已调用了 System.IO.Ports.SerialPort.Close()
        //     方法而发生。
        void DiscardOutBuffer();
        //
        // 摘要:
        //     打开一个新的串行端口连接。
        //
        // 异常:
        //   System.InvalidOperationException:
        //     指定的端口已打开。
        //
        //   System.ArgumentOutOfRangeException:
        //     此实例的一个或多个属性无效。例如，System.IO.Ports.SerialPort.Parity、System.IO.Ports.SerialPort.DataBits
        //     或 System.IO.Ports.SerialPort.Handshake 属性不是有效值；System.IO.Ports.SerialPort.BaudRate
        //     小于或等于零；System.IO.Ports.SerialPort.ReadTimeout 或 System.IO.Ports.SerialPort.WriteTimeout
        //     属性小于零且不是 System.IO.Ports.SerialPort.InfiniteTimeout。
        //
        //   System.ArgumentException:
        //     端口名称不是以“COM”开始的。- 或 -端口的文件类型不受支持。
        //
        //   System.IO.IOException:
        //     此端口处于无效状态。 - 或 - 尝试设置基础端口状态失败。例如，从此 System.IO.Ports.SerialPort 对象传递的参数无效。
        //
        //   System.UnauthorizedAccessException:
        //     对端口的访问被拒绝。
        void Open();
        //
        // 摘要:
        //     从 System.IO.Ports.SerialPort 输入缓冲区读取一些字节并将那些字节写入字节数组中指定的偏移量处。
        //
        // 参数:
        //   buffer:
        //     将输入写入到其中的字节数组。
        //
        //   offset:
        //     缓冲区数组中开始写入的偏移量。
        //
        //   count:
        //     要读取的字节数。
        //
        // 返回结果:
        //     读取的字节数。
        //
        // 异常:
        //   System.ArgumentNullException:
        //     传递的 buffer 为 null。
        //
        //   System.InvalidOperationException:
        //     指定的端口未打开。
        //
        //   System.ArgumentOutOfRangeException:
        //     offset 或 count 参数超出了所传递的 buffer 的有效区域。offset 或 count 小于零。
        //
        //   System.ArgumentException:
        //     offset 加上 count 大于 buffer 的长度。
        //
        //   System.TimeoutException:
        //     没有可以读取的字节。
        int Read(byte[] buffer, int offset, int count);
        //
        // 摘要:
        //     从 System.IO.Ports.SerialPort 输入缓冲区中读取大量字符，然后将这些字符写入到一个字符数组中指定的偏移量处。
        //
        // 参数:
        //   buffer:
        //     将输入写入到其中的字符数组。
        //
        //   offset:
        //     缓冲区数组中开始写入的偏移量。
        //
        //   count:
        //     要读取的字符数。
        //
        // 返回结果:
        //     读取的字符数。
        //
        // 异常:
        //   System.ArgumentException:
        //     offset 加上 count 大于缓冲区的长度。- 或 -count 为 1 并且缓冲区中有一个代理项字符。
        //
        //   System.ArgumentNullException:
        //     传递的 buffer 为 null。
        //
        //   System.ArgumentOutOfRangeException:
        //     offset 或 count 参数超出了所传递的 buffer 的有效区域。offset 或 count 小于零。
        //
        //   System.InvalidOperationException:
        //     指定的端口未打开。
        //
        //   System.TimeoutException:
        //     没有可以读取的字符。
        int Read(char[] buffer, int offset, int count);
        //
        // 摘要:
        //     从 System.IO.Ports.SerialPort 输入缓冲区中同步读取一个字节。
        //
        // 返回结果:
        //     强制转换为 System.Int32 的字节；或者，如果已读取到流的末尾，则为 -1。
        //
        // 异常:
        //   System.InvalidOperationException:
        //     指定的端口未打开。
        //
        //   System.ServiceProcess.TimeoutException:
        //     该操作未在超时时间到期之前完成。- 或 -未读取任何字节。
        int ReadByte();
        //
        // 摘要:
        //     从 System.IO.Ports.SerialPort 输入缓冲区中同步读取一个字符。
        //
        // 返回结果:
        //     读取的字符。
        //
        // 异常:
        //   System.InvalidOperationException:
        //     指定的端口未打开。
        //
        //   System.ServiceProcess.TimeoutException:
        //     该操作未在超时时间到期之前完成。- 或 -在分配的超时时间内没有可用的字符。
        int ReadChar();
        //
        // 摘要:
        //     在编码的基础上，读取 System.IO.Ports.SerialPort 对象的流和输入缓冲区中所有立即可用的字节。
        //
        // 返回结果:
        //     System.IO.Ports.SerialPort 对象的流和输入缓冲区的内容。
        //
        // 异常:
        //   System.InvalidOperationException:
        //     指定的端口未打开。
        string ReadExisting();
        //
        // 摘要:
        //     一直读取到输入缓冲区中的 System.IO.Ports.SerialPort.NewLine 值。
        //
        // 返回结果:
        //     输入缓冲区中直到首次出现 System.IO.Ports.SerialPort.NewLine 值的内容。
        //
        // 异常:
        //   System.InvalidOperationException:
        //     指定的端口未打开。
        //
        //   System.TimeoutException:
        //     该操作未在超时时间到期之前完成。- 或 -未读取任何字节。
        string ReadLine();
        //
        // 摘要:
        //     一直读取到输入缓冲区中的指定 value 的字符串。
        //
        // 参数:
        //   value:
        //     指示读取操作停止位置的值。
        //
        // 返回结果:
        //     输入缓冲区中直到指定 value 的内容。
        //
        // 异常:
        //   System.ArgumentException:
        //     value 参数的长度为 0。
        //
        //   System.ArgumentNullException:
        //     value 参数为 null。
        //
        //   System.InvalidOperationException:
        //     指定的端口未打开。
        //
        //   System.TimeoutException:
        //     该操作未在超时时间到期之前完成。
        string ReadTo(string value);
        //
        // 摘要:
        //     将指定的字符串写入串行端口。
        //
        // 参数:
        //   text:
        //     输出字符串。
        //
        // 异常:
        //   System.InvalidOperationException:
        //     指定的端口未打开。
        //
        //   System.ArgumentNullException:
        //     str 为 null。
        //
        //   System.ServiceProcess.TimeoutException:
        //     该操作未在超时时间到期之前完成。
        void Write(string text);
        //
        // 摘要:
        //     使用缓冲区的数据将指定数量的字节写入串行端口。
        //
        // 参数:
        //   buffer:
        //     包含要写入端口的数据的字节数组。
        //
        //   offset:
        //     buffer 参数中从零开始的字节偏移量，从此处开始将字节复制到端口。
        //
        //   count:
        //     要写入的字节数。
        //
        // 异常:
        //   System.ArgumentNullException:
        //     传递的 buffer 为 null。
        //
        //   System.InvalidOperationException:
        //     指定的端口未打开。
        //
        //   System.ArgumentOutOfRangeException:
        //     offset 或 count 参数超出了所传递的 buffer 的有效区域。offset 或 count 小于零。
        //
        //   System.ArgumentException:
        //     offset 加上 count 大于 buffer 的长度。
        //
        //   System.ServiceProcess.TimeoutException:
        //     该操作未在超时时间到期之前完成。
        void Write(byte[] buffer, int offset, int count);
        //
        // 摘要:
        //     使用缓冲区的数据将指定数量的字符写入串行端口。
        //
        // 参数:
        //   buffer:
        //     包含要写入端口的数据的字符数组。
        //
        //   offset:
        //     buffer 参数中从零开始的字节偏移量，从此处开始将字节复制到端口。
        //
        //   count:
        //     要写入的字符数。
        //
        // 异常:
        //   System.ArgumentNullException:
        //     传递的 buffer 为 null。
        //
        //   System.InvalidOperationException:
        //     指定的端口未打开。
        //
        //   System.ArgumentOutOfRangeException:
        //     offset 或 count 参数超出了所传递的 buffer 的有效区域。offset 或 count 小于零。
        //
        //   System.ArgumentException:
        //     offset 加上 count 大于 buffer 的长度。
        //
        //   System.ServiceProcess.TimeoutException:
        //     该操作未在超时时间到期之前完成。
        void Write(char[] buffer, int offset, int count);
        //
        // 摘要:
        //     将指定的字符串和 System.IO.Ports.SerialPort.NewLine 值写入输出缓冲区。
        //
        // 参数:
        //   text:
        //     写入输出缓冲区的字符串。
        //
        // 异常:
        //   System.ArgumentNullException:
        //     str 参数为 null。
        //
        //   System.InvalidOperationException:
        //     指定的端口未打开。
        //
        //   System.TimeoutException:
        //     System.IO.Ports.SerialPort.WriteLine(System.String) 方法无法写入流。
        void WriteLine(string text);
    }
}
